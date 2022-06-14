﻿using Handymand.Models;
using Handymand.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public interface IJobOfferService
    {
        public List<JobOfferDTO> AllJobOffers();

        public Task<JobOfferDTO> Create(JobOfferDTO dto);

        public JobOfferDTO GetById(int Id);

        public Task<string> TestAsyncMethod(string fileName);
        public Task<string> TestAsyncMethod2(string fileName);
        public Task<string> TestAsyncMethod3(string fileName);
        public Task SaveJobOffersImages(List<IFormFile> files, int N);

    }
}
