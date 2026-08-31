using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using NIGA.Centrum.Entity.DataModels;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace NIGA.Centrum.Business.Implementation
{
    public class MonogramService : IMonoGramService
    {
        NIGACentrumContext context;

        public MonogramService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public MonoGramModel GetMonoGramById(long MonogramId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            MonoGramModel monogramList = new MonoGramModel();
            var monogramEntity = context.Monogram.FirstOrDefault(x => x.MonogramId == MonogramId && x.IsActive == true);
            // var monogramDetail = context.MonogramDetails.Where(x => x.MonogramId == MonogramId && x.IsDelete == false);
            var monogramDetail = (from monogramdetails in context.MonogramDetails
                                  join subsection in context.SubSectionMaster
                                  on monogramdetails.SubsectionId equals subsection.SubSectionId
                                  where monogramdetails.MonogramId == MonogramId && monogramdetails.IsDelete == false
                                  select new
                                  {
                                      monogramdetails.MonogramDetailId,
                                      monogramdetails.MonogramId,
                                      monogramdetails.SubsectionId,
                                      subsection.SubSectionName,
                                      monogramdetails.IsDelete
                                  }).Distinct().ToList();

            if (monogramEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Monogram not found";
            }

            monogramList.MonogramId = monogramEntity.MonogramId;
            monogramList.Monogram1 = monogramEntity.Monogram1;
            monogramList.Keywords = monogramEntity.Keywords;
            monogramList.EnteredBy = monogramEntity.EnteredBy;
            monogramList.EnteredDate = monogramEntity.EnteredDate;
            monogramList.ChangedBy = monogramEntity.ChangedBy;
            monogramList.ChangedDate = monogramEntity.ChangedDate;
            monogramList.IsActive = monogramEntity.IsActive;
            if (monogramDetail != null)
            {
                foreach (var item in monogramDetail)
                {
                    MonoGramDetailsListModel detail = new MonoGramDetailsListModel();
                    detail.MonogramDetailId = item.MonogramDetailId;
                    detail.SubsectionId = item.SubsectionId;
                    detail.IsDelete = item.IsDelete;
                    detail.SubsectionName = item.SubSectionName;

                    monogramList.ModelEx.Add(detail);
                }
            }

            return monogramList;

        }

        public List<MonoGramModel> GetMonogram(ref ErrorResponseModel errorResponseModel)
        {
            var MonogramList = new List<MonoGramModel>();
            errorResponseModel = new ErrorResponseModel();
            var MonogramEntityList = context.Monogram.Where(x => x.IsActive == true).OrderBy(x => x.MonogramId).ToList();
            if (MonogramEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Monogram not found";
            }

            MonogramEntityList.ForEach(item =>
            {
                MonogramList.Add(new MonoGramModel
                {
                    MonogramId = item.MonogramId,
                    Monogram1 = item.Monogram1,
                    Keywords = item.Keywords,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    IsActive = item.IsActive,
                });
            });
            return MonogramList;
        }

        public string DeleteMonogram(MonoGramModel monoGramModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var monogramEntity = context.Monogram.FirstOrDefault(x => x.MonogramId == monoGramModel.MonogramId);
            if (monogramEntity != null)
            {
                monogramEntity.IsActive = monoGramModel.IsActive;
                monogramEntity.ChangedBy = monoGramModel.EnteredBy;
                monogramEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();

                var monogramDetailList = context.MonogramDetails.Where(x => x.MonogramId == monoGramModel.MonogramId).ToList();

                foreach (var item1 in monogramDetailList)
                {
                    item1.IsDelete = true;
                    context.SaveChanges();
                }
                Message = "Monogram Deleted Successfully";
            }

            return Message;
        }

        public string SaveMonogram(MonoGramModel monoGramModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            //foreach (var item in monoGramModel)
            //{
            DateTime currentDateTime = DateTime.Now;
            if (monoGramModel.MonogramId == 0)
            {
                Monogram monogramEntity = new Monogram();
                // monogramEntity.MonogramId = monoGramModel.MonogramId;
                monogramEntity.Monogram1 = monoGramModel.Monogram1;
                monogramEntity.Keywords = monoGramModel.Keywords;
                monogramEntity.EnteredBy = monoGramModel.EnteredBy;
                monogramEntity.ChangedBy = monoGramModel.ChangedBy;
                monogramEntity.ChangedDate = monoGramModel.ChangedDate;
                monogramEntity.EnteredDate = currentDateTime;
                monogramEntity.IsActive = monoGramModel.IsActive;
                context.Monogram.Add(monogramEntity);
                context.SaveChanges();

                foreach (var item1 in monoGramModel.ModelEx)
                {
                    MonogramDetails modeldetails = new MonogramDetails();
                    modeldetails.MonogramId = monogramEntity.MonogramId;
                    modeldetails.SubsectionId = item1.SubsectionId;
                    modeldetails.IsDelete = false;
                    context.MonogramDetails.Add(modeldetails);
                    context.SaveChanges();
                    Message = "Monogram Details Saved Successfully";
                }
                //Message = "Monogram Details Saved Successfully";
            }
            else
            {
                var monogramEntity = context.Monogram.FirstOrDefault(x => x.MonogramId == monoGramModel.MonogramId);
                if (monogramEntity != null)
                {
                    monogramEntity.Monogram1 = monoGramModel.Monogram1;
                    monogramEntity.Keywords = monoGramModel.Keywords;
                    monogramEntity.EnteredBy = monoGramModel.EnteredBy;
                    monogramEntity.ChangedBy = monoGramModel.ChangedBy;
                    monogramEntity.ChangedDate = monoGramModel.ChangedDate;
                    monogramEntity.EnteredDate = currentDateTime;
                    monogramEntity.IsActive = monoGramModel.IsActive;
                    context.SaveChanges();

                    var monogramDetailList = context.MonogramDetails.Where(x => x.MonogramId == monoGramModel.MonogramId && x.IsDelete == false).ToList();
                    foreach (var itemDetail in monogramDetailList)
                    {
                        itemDetail.IsDelete = true;
                        context.SaveChanges();
                    }
                    //context.Remove(monogramDetailList);
                    //context.SaveChanges();

                    foreach (var item1 in monoGramModel.ModelEx)
                    {
                        // var monogramDetailexist= context.MonogramDetails.Where(x => x.MonogramId == item1.MonogramDetailId).ToList();
                        //if (monogramDetailexist != null)
                        {
                            if (item1.MonogramDetailId != 0)//---Update 
                            {
                                var monogramDetailexist = context.MonogramDetails.FirstOrDefault(x => x.MonogramDetailId == item1.MonogramDetailId);
                                monogramDetailexist.IsDelete = false;
                                context.SaveChanges();
                            }
                            //}
                            else//---Add
                            {
                                MonogramDetails modeldetails = new MonogramDetails();
                                modeldetails.MonogramId = monogramEntity.MonogramId;
                                modeldetails.SubsectionId = item1.SubsectionId;
                                modeldetails.IsDelete = false;
                                context.MonogramDetails.Add(modeldetails);
                                context.SaveChanges();

                            }

                        }

                    }
                    Message = "Monogram Details Updated Successfully";
                }

                // }
            }
            return Message;
        }
    }
}
