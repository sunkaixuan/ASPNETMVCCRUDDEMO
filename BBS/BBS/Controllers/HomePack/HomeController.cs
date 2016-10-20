﻿using BBS.Infrastructure.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SyntacticSugar;
using BBS.Infrastructure.DbModels;
using SqlSugar;
using BBS.Infrastructure.ViewModels;

namespace BBS.Controllers.HomePack
{
    public class HomeController : BaseController
    {
        public HomeController(DbService service) : base(service) {
        }

        /// <summary>
        /// 写的很简单只是让你能够理解如何分层
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new ResultModel<string, List<List<V_dnt_test_topics>>>();

            _service.Command<Outsourcing>((db/*数据服务对象*/, o /*外包服务对象*/) =>
            {
                var allTopic = db.Queryable<dnt_test_topics>()
                .JoinTable<dnt_test_forums>((t, f) => t.fid == f.fid)
                .Select<V_dnt_test_topics>("t.*,f.name as typeName").ToList(); //获取贴子

                //首页title
                model.ResultInfo = "BBS首页";

                //将贴子进行分组处理
                model.ResultInfo2 = o.ArrangeTopic(allTopic);
            });

            return View(model);
        }
    }
}