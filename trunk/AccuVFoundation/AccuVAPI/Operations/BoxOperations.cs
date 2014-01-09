//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AccuVAPI.Models;
//using BoxApi.V2;
//using Xplore.Framework.Common.DataAccessLayer;

//namespace AccuVAPI.Operations
//{
//    public class BoxOperations : OperationStore
//    {
//        public BoxOperations(Rollout_TrackingContext db)
//            : base(db)
//        {
            
//        }

//        public string AccessToken { get; set; }

//        /// <summary>
//        /// Method is returning the list of required documents per Site and Task ID
//        /// The result set will be displayed in the "Documents" Tab 
//        /// </summary>
//        /// <param name="taskId"></param>
//        /// <param name="siteNumber"></param>
//        /// <param name="projectId"></param>
//        /// <returns></returns>
//        public DMResponseDTO<List<DMRequiredDocumentDTO>> DMRequiredDocuments(string taskId, string siteNumber, string projectId)
//        {
//            var result = new DMResponseDTO<List<DMRequiredDocumentDTO>> {Code = DMResponseDTO.CODE_OK};

//            try
//            {
//                // create the parameters for the Stored Procedure - usp_att_closeout_documents_needed

//                var pSiteNumber = new SqlParameter("p_site_number", siteNumber);
//                var pTaskId = new SqlParameter("task_id", taskId);
//                var pProjectId = new SqlParameter("project_id", projectId);

//                // Execute the stored procedure and create a list from it
//                var docsNeeded = _db.Database.SqlQuery<DMRequiredDocumentDTO>("usp_att_closeout_documents_needed @p_site_number, @task_id, @project_id", pSiteNumber, pTaskId, pProjectId);
//                var docsNeededEnum = docsNeeded.GetEnumerator();
//                List<DMRequiredDocumentDTO> docsNeededList = new List<DMRequiredDocumentDTO>();
//                while (docsNeededEnum.MoveNext())
//                {
//                    docsNeededList.Add(docsNeededEnum.Current);
//                }

//                result.Result = docsNeededList;
//            }
//            catch (Exception ex)
//            {
//                result.Code = DMResponseDTO.CODE_GENERIC_ERROR;
//                result.Description = ex.Message;
//            }

//            return result;
//        }

//        public DMResponseDTO<List<DMDocumentDTO>> DMGetCurrentDocuments(string docReqID)
//        {

//            var result = new DMResponseDTO<List<DMDocumentDTO>> {Code = DMResponseDTO.CODE_OK};

//            try
//            {
//                var pDocReqID = new SqlParameter("p_doc_req_id", docReqID);

//                // Execute the stored procedure and create a list from it
//                var currentDocs = _db.Database.SqlQuery<DMDocumentDTO>("usp_att_closeout_documents_uploaded @p_doc_req_id", pDocReqID);
//                var currentDocsEnum = currentDocs.GetEnumerator();
//                List<DMDocumentDTO> currentDocsList = new List<DMDocumentDTO>();
//                while (currentDocsEnum.MoveNext())
//                {
//                    currentDocsList.Add(currentDocsEnum.Current);
//                }

//                result.Result = currentDocsList;
//            }
//            catch (Exception ex)
//            {
//                result.Code = DMResponseDTO.CODE_GENERIC_ERROR;
//                result.Description = ex.Message;
//            }

//            return result;
//        }

//        public DMResponseDTO<string> DMDeleteDocument(DMDocumentDTO fileData, int docReqId)
//        {

//            var result = new DMResponseDTO<string> { Code = DMResponseDTO.CODE_OK };

//            try
//            {
//                var ticks = DateTime.Now.Ticks.ToString();

//                // First, remove the file from the Box.net 
//                // By removing a file, we actually rename it using the ".DELETED" extension
//                BoxManager boxManager = new BoxManager(AccessToken);
//                boxManager.RenameFile(fileData.file_id, string.Format("{0}.Deleted{1}", fileData.file_name, ticks));

//                // After the file has been removed from Box.net
//                // We should also remove it from the DB by calling a stored procedure
//                IStoredProcedure procedure = DataAccess.CreateStoredProcedure("usp_att_closeout_del_uploaded_doc"); ;
//                procedure.AddInputIntegerParameter("p_doc_req_id", docReqId);
//                procedure.AddInputStringParameter("p_file_name", fileData.file_name);
//                procedure.AddInputStringParameter("p_timestamp", ticks);
//                procedure.AddOutputStringParameter("p_error");

//                procedure.Execute(DataAccess.GetXploreGeneralConnectionString, ErrorResultType.ASSERTION);

//                string p_error = procedure.GetOutputStringParameter("p_error");
//                if (!string.IsNullOrEmpty(p_error))
//                {
//                    throw new Exception(p_error);
//                }

//                // The delete is complete, return the file name to the client
//                result.Result = fileData.file_name;
//            }
//            catch (Exception ex)
//            {
//                result.Code = DMResponseDTO.CODE_GENERIC_ERROR;
//                result.Description = ex.Message;
//            }

//            return result;
//        }

//    }
//}
