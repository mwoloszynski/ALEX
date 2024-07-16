using BLL.Helpers;
using COMMON.DTOs;
using DAL.Repositories;
using DBMODEL.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StockUpdaterService : BackgroundService
    {
        private readonly ExchangeRepository<Exchange> _exchangeRepository;
        private readonly StockRepository<Stock> _stockRepository;
        private readonly StockResultRepository<StockResult> _stockResultRepository;
        private readonly static HttpClient client = new HttpClient();
        private static string path = "https://eodhd.com/api/exchanges-list/?api_token={0}&fmt=json";
        private static string path2 = "https://eodhd.com/api/real-time/{0}?api_token={1}&fmt=json";
        private static string token = "demo";
        //private static string token = "6696414156bce8.98623306";
        private static string testStock = "AAPL.US";

        public StockUpdaterService()
        {
           _exchangeRepository = new ExchangeRepository<Exchange>();
            _stockRepository = new StockRepository<Stock>();
            _stockResultRepository = new StockResultRepository<StockResult>();
        }


        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            DownloadData();

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(30));

            try
            {
                while(await timer.WaitForNextTickAsync(stoppingToken))
                {
                    DownloadData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void DownloadData()
        {
            var pathformatted = string.Format(path2, testStock, token);
            HttpResponseMessage responseMessage = await client.GetAsync(pathformatted);
            if (responseMessage.IsSuccessStatusCode)
            {
                var stockResultDTO = await responseMessage.Content.ReadFromJsonAsync<StockResultDTO>();
                if (stockResultDTO == null) return;
                var stock = _stockRepository.GetAll(x => x.Code == stockResultDTO.code).FirstOrDefault();
                if(stock == null) return;
                var stockResult = stockResultDTO.ToStockResult(stock.Id);
                await _stockResultRepository.Add(stockResult);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

    }
}
