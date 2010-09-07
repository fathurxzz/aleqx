//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.Common.Utils; 

namespace NopSolutions.NopCommerce.Web.Administration.Modules
{
    public partial class SpecificationAttributeDetailsControl : BaseNopAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SelectTab(this.AttributeTabs, this.TabId);
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SpecificationAttribute specificationAttribute = ctrlSpecificationAttributeInfo.SaveInfo();
                    ctrlSpecificationAttributeOptions.SaveInfo();

                    CustomerActivityManager.InsertActivity(
                        "EditSpecAttribute",
                        GetLocaleResourceString("ActivityLog.EditSpecAttribute"),
                        specificationAttribute.Name);

                    if (specificationAttribute != null)
                        Response.Redirect(string.Format("SpecificationAttributeDetails.aspx?SpecificationAttributeID={0}&TabID={1}", specificationAttribute.SpecificationAttributeId, this.GetActiveTabId(this.AttributeTabs)));
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                SpecificationAttribute specificationAttribute = SpecificationAttributeManager.GetSpecificationAttributeById(this.SpecificationAttributeId);
                if (specificationAttribute != null)
                {
                    SpecificationAttributeManager.DeleteSpecificationAttribute(this.SpecificationAttributeId);

                    CustomerActivityManager.InsertActivity(
                        "DeleteSpecAttribute",
                        GetLocaleResourceString("ActivityLog.DeleteSpecAttribute"),
                        specificationAttribute.Name);
                }

                Response.Redirect("SpecificationAttributes.aspx");
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }
        }

        public int SpecificationAttributeId
        {
            get
            {
                return CommonHelper.QueryStringInt("SpecificationAttributeId");
            }
        }

        protected string TabId
        {
            get
            {
                return CommonHelper.QueryString("TabId");
            }
        }
    }
}