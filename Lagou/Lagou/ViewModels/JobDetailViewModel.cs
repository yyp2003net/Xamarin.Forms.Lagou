﻿using Caliburn.Micro;
using Lagou.API.Entities;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class JobDetailViewModel : BaseVM {
        public override string Title {
            get {
                return "职位详情";
            }
        }

        public Position Data { get; set; }

        public BindableCollection<EvaluationViewModel> Evaluations {
            get; set;
        } = new BindableCollection<EvaluationViewModel>();

        public bool HasEvaluations { get; set; }

        public bool NotHaveEvaluations { get; set; }

        public int ID { get; set; }


        public JobDetailViewModel() {

        }

        protected async override void OnActivate() {
            base.OnActivate();

            var mth = new PositionDetail() {
                PositionID = this.ID
            };
            this.Data = await API.ApiClient.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Data);

            var mth2 = new EvaluationList() {
                PositionID = this.ID
            };
            var evs = await API.ApiClient.Execute(mth2);
            this.Evaluations.AddRange(evs.Select(e => new EvaluationViewModel(e)));
            this.NotifyOfPropertyChange(() => this.Evaluations);

            this.HasEvaluations = this.Evaluations.Count > 0;
            this.NotHaveEvaluations = !this.HasEvaluations;
            this.NotifyOfPropertyChange(() => this.HasEvaluations);
            this.NotifyOfPropertyChange(() => this.NotHaveEvaluations);
        }
    }
}