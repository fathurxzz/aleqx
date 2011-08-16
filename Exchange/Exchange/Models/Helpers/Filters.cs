using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        public static IEnumerable<Currency> ExtractCurrency(IEnumerable<ICurrencyFilter> source)
        {
            var cList = new List<Currency>{
                new Currency
                {
                    CurId = 0,
                    CurName = GetResourceString("All"),
                    Selected = WebSession.SelectedCurrency == 0
                    }
                };

            cList.AddRange(source.Select(t =>
                new Currency
                {
                    CurDef = t.CurDef,
                    CurId = t.CurId,
                    CurName = t.CurName,
                    Selected = t.CurId == WebSession.SelectedCurrency
                }).Distinct().ToList());

            return cList;
        }


        public static IEnumerable<Oper> ExtractOperation(IEnumerable<IOperationFilter> source)
        {
            var oList = new List<Oper>
                            {
                                new Oper
                                    {
                                        Operation = 0,
                                        OperName = GetResourceString("All"),
                                        Selected = (int) WebSession.Operation == 0
                                    }
                            };

            oList.AddRange(source.Select(t => new Oper
                                              {
                                                  Operation = t.Operation,
                                                  OperName = t.OperName,
                                                  Selected = t.Operation == WebSession.Operation
                                              }).Distinct().ToList());
            return oList;
        }

        public static IEnumerable<OperationSign> ExtractOperationSign(IEnumerable<IOperSignFilter> source)
        {
            var oList = new List<OperationSign>
                            {
                                new OperationSign()
                                    {
                                        OperSign = 0,
                                        OperSignName = GetResourceString("All"),
                                        Selected = (int) WebSession.Operation == 0
                                    }
                            };

            oList.AddRange(source.Select(t => new OperationSign
            {
                OperSign = t.OperSign,
                OperSignName = t.OperSignName,
                Selected = t.OperSign == WebSession.OperSign
            }).Distinct().ToList());
            return oList;
        }

        public static IEnumerable<SelectListItem> ExctractCourse(IEnumerable<ICourseFilter> source)
        {

            

            List<string> courses =
                source.OrderBy(c => c.CourseOrient)
                .Select(
                    c => c.CourseMorSign
                        ? "MOR"
                        : Convert.ToString(c.CourseOrient))
                .Distinct()
                .ToList();


            if(WebSession.Course!="all" && !courses.Any(c=>c.Match(WebSession.Course)))
            {
                WebSession.Course = "all";
            }

            var cList = new List<SelectListItem>
                            {
                                new SelectListItem
                                    {
                                        Text = "---",
                                        Value = "all",
                                        Selected = WebSession.Course == "all"
                                    }
                            };
            cList.AddRange(
                courses.Select(
                    course =>
                    new SelectListItem
                        {
                            Text = course.ToString(),
                            Value = course.ToString(),
                            Selected = WebSession.Course.Match(course)
                        }));
            return cList;
        }

    }


}