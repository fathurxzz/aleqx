using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Backload;
using Backload.Eventing.Args;

namespace Mayka.Areas.Admin.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /Admin/FileUpload/

        public async Task<ActionResult> FileHandler()
    {
        FileUploadHandler handler = new FileUploadHandler(Request, this);
        //handler.IncomingRequestStarted += handler_IncomingRequestStarted;

        //handler.AuthorizeRequestStarted += handler_AuthorizeRequestStarted;
        //handler.AuthorizeRequestFinished += handler_AuthorizeRequestFinished;

        //handler.GetFilesRequestStarted += handler_GetFilesRequestStarted;
        //handler.GetFilesRequestFinished += handler_GetFilesRequestFinished;
        //handler.GetFilesRequestException += handler_GetFilesRequestException;

            handler.StoreFileRequestStartedAsync += handler_StoreFileRequestStartedAsync;
        handler.StoreFileRequestFinished += handler_StoreFileRequestFinished;
        //handler.StoreFileRequestException += handler_StoreFileRequestException;

        //handler.DeleteFilesRequestStarted += handler_DeleteFilesRequestStarted;
        //handler.DeleteFilesRequestFinishedAsync += handler_DeleteFilesRequestFinishedAsync; 
        //handler.DeleteFilesRequestException += handler_DeleteFilesRequestException;

        //handler.OutgoingResponseCreated += handler_OutgoingResponseCreated;

        //handler.ProcessPipelineExceptionOccured += handler_ProcessPipelineExceptionOccured;


        ActionResult result = await handler.HandleRequestAsync();
        return result;
    }


        async Task handler_StoreFileRequestStartedAsync(object sender, StoreFileRequestEventArgs e)
        {
            var fileName = e.Param.FileStatusItem.FileName;
            if (fileName.Equals("some_bad_name.tmp", StringComparison.OrdinalIgnoreCase))
            {
                fileName = "some_good_name.tmp";

                e.Param.FileStatusItem.FileName = fileName;
                e.Param.FileStatusItem.UpdateStatus(true);
            }
        }

        void handler_StoreFileRequestFinished(object sender, StoreFileRequestEventArgs e)
        {
            var fileName = e.Param.FileStatusItem.FileName;
            var folder = e.Param.FileStatusItem.StorageInfo.FileDirectory;

            //DoSomeOperations(folder, fileName);
        }


    }
}
