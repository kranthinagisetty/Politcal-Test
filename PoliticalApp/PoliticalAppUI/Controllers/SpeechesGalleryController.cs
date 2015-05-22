using Google.GData.Client;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.MediaRss;
using Google.GData.YouTube;
using Google.YouTube;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using NReco.VideoConverter;
using PagedList;
using PoliticalAppRepository;
using PoliticalAppUI.Models;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliticalAppUI.Controllers
{
    public class SpeechesGalleryController : BaseController
    {
        //
        // GET: /SpeechesGallery/


        #region Private Members

        private IActivitiesRepository _ActRepository;


        #endregion

        public SpeechesGalleryController(IActivitiesRepository ActRepository)
        {
            _ActRepository = ActRepository;
        }
        // GET: SpeechesGallery
        public ActionResult Index(int page = 1, int page1 = 1)
        {
            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }

            if (page1 < 1)
            {
                page1 = 1;
            }

            AudioVideoModel Speeches = new AudioVideoModel();

            int pageSize = 5;
           
            //Used the following two formulas so that it doesn't round down on the returned integer
            decimal AudiototalPages = ((decimal)(_ActRepository.GetAudioSpeeches().Count() / (decimal)pageSize));
            decimal VideototalPages = ((decimal)(_ActRepository.GetVideoSpeeches().Count() / (decimal)pageSize));

            Speeches.audioList = _ActRepository.GetAudioSpeeches().ToPagedList(page, pageSize);
            Speeches.videoList = _ActRepository.GetVideoSpeeches().ToPagedList(page1, pageSize);

            ViewBag.AudioTotalPages = Math.Ceiling(AudiototalPages);
            ViewBag.VideoTotalPages = Math.Ceiling(VideototalPages);

            ViewBag.PageNumberAudio = page;
            ViewBag.PageNumberVideo = page1;

            if (page > 1)
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("SpeechesAudioList", Speeches) : View(Speeches);
            else
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("SpeechesVideoList", Speeches) : View(Speeches);
            
        }       

        public ActionResult Gallery()
        {
            GalleryModel galleryModel = new GalleryModel();

            galleryModel.drpAlbumList = new SelectList(_ActRepository.GetAlbumList(), "AlbumId", "AlbumName");


            return View(galleryModel);
        }

        [HttpPost]
        public ActionResult UploadGallery(HttpPostedFileBase galleryFile, string AlbumName, string AlbumDescription, string Place, string drpAlbum)
        {

            
            HttpFileCollectionBase Files = Request.Files;

            bool fileSaved = false;

            foreach (string h in Files.AllKeys)
            {
                dynamic file = Files[h];

                if (Files[0].ContentLength > 0)
                {
                    string fileName = Files[0].FileName;
                    int fileSize = Files[0].ContentLength;
                    string type = Files[0].ContentType.Split('/')[0].ToString();

                    if (type == "video" || type == "image")
                    {
                        Gallery _Gallery = new Gallery();
                        if (type == "video")
                        {
                           _Gallery.IsVideo = true;
                           _Gallery.AlbumLink  = UploadVideoToYoutube(galleryFile, AlbumDescription,type); 
                        }
                        else
                        {
                            _Gallery.IsVideo = false;
                            _Gallery.AlbumLink = UploadFileToAzure(galleryFile);
                        }

                        if (Session["User"] != null)
                        {
                            Politician politician = Session["User"] as Politician;
                            _Gallery.PoliticianID = politician.PoliticianID;
                        }
                        else
                        {
                            return RedirectToAction("Index", "Login");
                        }

                        
                        try
                        {
                            _Gallery.CreatedDate = DateTime.UtcNow;
                            _Gallery.ModifiedDate = DateTime.UtcNow;
                            _Gallery.AlbumDescription = AlbumDescription;
                            _Gallery.AlbumName = AlbumName;
                            _Gallery.Place = Place;
                          

                            int insRec = _ActRepository.InsertGallery(_Gallery);
                            //Get & Save the File
                            if (insRec > 0)
                            {                               
                                fileSaved = true;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        // Throw Error file type is not valid  
                        ViewBag.Validation = "File type is not valid";
                    }
                }
            }
           // return View("Gallery");
            return RedirectToAction("Gallery", "SpeechesGallery");
        }
       
        public static bool DeleteVideo(string VideoId)
        {
            try
            {
                YouTubeRequestSettings settings;
                YouTubeRequest request;
                string devkey = "YOUR DEVELOPPER KEY HERE";
                string username = "Your Youtube Username";
                string password = "Your Youtube Password";
                settings = new YouTubeRequestSettings("Your Application Name", devkey, username, password) { Timeout = -1 };
                request = new YouTubeRequest(settings);

                Uri videoEntryUrl = new Uri(String.Format("http://gdata.youtube.com/feeds/api/users/{0}/uploads/{1}", username, VideoId));
                Video video = request.Retrieve<Video>(videoEntryUrl);
                request.Delete(video);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string UploadFileToAzure(HttpPostedFileBase fileBase)
        {
            Boolean returnVal = true;
            string upURL = string.Empty;
            try
            {
                if (fileBase != null && fileBase.ContentLength > 0)
                {
                    #region azure storage

                    string AccountName = "politicalappstorage";
                    string AccountKey = "V7oB7mpmt0F/dFqVCs/7bTP+6XNrBKLB3CEm0gl70/xV+0hoLvCN+x8rKsMzvOTZJeKVK6UVIHokgM7tauAwGg==";


                    StorageCredentials creds = new StorageCredentials(AccountName, AccountKey);
                    CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                    CloudBlobClient client = account.CreateCloudBlobClient();

                    var type = fileBase.ContentType.Split('/')[0].ToString();

                    CloudBlobContainer sampleContainer = client.GetContainerReference(type.ToLower());

                    sampleContainer.CreateIfNotExists();
                    CloudBlockBlob azureBlockBlob = sampleContainer.GetBlockBlobReference(fileBase.FileName);

                    var perm = new BlobContainerPermissions();
                    perm.PublicAccess = BlobContainerPublicAccessType.Blob;
                    sampleContainer.SetPermissions(perm);   


                    if (!azureBlockBlob.Exists())
                    {
                        azureBlockBlob.UploadFromStream(fileBase.InputStream);
                        // azureBlockBlob.Properties.ContentType = tenderDocument.PostedFile.ContentType;
                        azureBlockBlob.SetProperties();
                        upURL = azureBlockBlob.StorageUri.PrimaryUri.ToString();
                        //Primary = 'https://politicalappstorage.blob.core.windows.net/politicalcontainer/Tulips.jpg'; Secondary = 'https://politicalappstorage-secondary.blob.core.windows.net/politicalcontainer/Tulips.jpg'
                    }
                    else
                    {
                       return "File already Exists";
                    }

                    #endregion
                }
                ViewBag.status = returnVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

            return upURL;
        }

        public string UploadVideoToYoutube(HttpPostedFileBase fileBase,  string Desc="", string type="video", string Title = "")
        {
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                try
                {
                    #region upload video to youtube
                    if (fileBase != null && fileBase.ContentLength > 0)
                    {
                        // Get file info

                        string _filepath = System.IO.Path.GetFullPath(fileBase.FileName);

                        var fileName = Path.GetFileName(fileBase.FileName);

                        var filePath = Path.GetFullPath(fileBase.FileName);


                        YouTubeRequestSettings _settings1;
                        YouTubeRequest _request1;

                        string username = "kranthi.ascendia@gmail.com";
                        string password = "kumaran123#";
                        
                        Google.YouTube.Video newVideo = new Google.YouTube.Video();                       

                        const string developerKey = "AI39si5PwHJ3WWWCqVKQkiQm8cNsdtdieo794fLioo7_2TUftQEY9AO5ZWsoUCRAfaMcYG5Xi7zY4ZNEBYuvIzt5VbxJLLbkBA";
                        const string applicationName = "political";
                          _settings1 = new YouTubeRequestSettings(applicationName,  developerKey,username,password);  
                          _request1 = new YouTubeRequest(_settings1);


                       newVideo.Title = Title;
                       newVideo.Keywords = "Politics";
                       newVideo.Description = Desc;
                       newVideo.YouTubeEntry.Private = false;                     
                       newVideo.YouTubeEntry.Location = new GeoRssWhere(71, -111);
                       newVideo.Tags.Add(new MediaCategory("Sports", YouTubeNameTable.CategorySchema));

                      // newVideo.Tags.Add(new MediaCategory("politicalVideoTags", YouTubeNameTable.DeveloperTagSchema));

                      // var filepath = "D:\\SATEESH\\Videos\\1sdfad.3gp";
                       type = fileBase.ContentType;
                       var filepath = Path.Combine(Server.MapPath("~/Uploads/" + type + "/"));

                       if (!Directory.Exists(filepath))
                       {
                           Directory.CreateDirectory(filepath);
                       }
                       fileBase.SaveAs(filepath + fileName);

                       var _path = filepath + fileName;

                        newVideo.YouTubeEntry.MediaSource = new MediaFileSource(_path, fileBase.ContentType);
                        var createdVideo = _request1.Upload(newVideo);
                        System.IO.File.Delete(_path);

                        return createdVideo.VideoId.ToString();
                    }

                    #endregion
                }
                catch (Google.GData.Client.GDataRequestException ex)
                {
                    
                }
            }
            
           
            return "";
        }

        [HttpPost]
        public ActionResult UploadVideoAudio(HttpPostedFileBase file, string Title, string Description,string Place)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    var type = file.ContentType.Split('/')[0].ToString();

                    int MaxContentLength = 1024 * 1024 * 10; //10 MB

                    //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                    //if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    //{
                    //    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    //}
                     if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        #region inm

                        Speech _Speech = new Speech();
                        if (type == "audio")
                        {
                            _Speech.IsVideo = false;
                            _Speech.SpeechLink = UploadFileToAzure(file).ToString();
                        }
                        else if (type == "video")
                        {
                            _Speech.IsVideo = true;
                            _Speech.SpeechLink = UploadVideoToYoutube(file, Title, Description, type);
                        }
                        else
                        {
                            return View();
                        }

                        if (Session["User"] != null)
                        {
                            Politician politician = Session["User"] as Politician;
                            _Speech.PoliticianID = politician.PoliticianID;
                        }
                        else
                        {
                            return RedirectToAction("Index", "Login");
                        }
                        _Speech.CreatedDate = DateTime.UtcNow;
                        _Speech.ModifiedDate = DateTime.UtcNow;
                        _Speech.SpeechDescription = Description;
                        _Speech.SpeechName = string.IsNullOrEmpty(Title)?"":Title;
                        _Speech.Place = Place;                       

                        int insRec = _ActRepository.InsertAudioVideo(_Speech);


                        #endregion
                       
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                    }
                   
                }
            }
            return View();
        }
    }

   

}

