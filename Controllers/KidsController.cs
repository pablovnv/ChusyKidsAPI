using ChusyKidsAPI.Model;
using ChusyKidsAPI.Models;
using ChusyKidsAPI.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChusyKidsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KidsController : ControllerBase
    {
        [HttpPost]
        public async Task<SendModel> GetAsync([FromBody]ReciveModel json)
        {
            //var dateRequest = Request.Headers[ResourceResponse.Date].ToString();
            //var dateRecive = json.queryResult.queryText;
            var dateRequest = "7-5-2022";

            if (string.IsNullOrEmpty("7-5-2022"))
                return CreateSend(ResourceResponse.PonFecha);

            var result = LogicKids(DateTime.Parse(dateRequest));

            return CreateSend(result);

            return new SendModel();
        }

        private string LogicKids(DateTime date)
        {
            var mondayThisWeek = GetMonday(DateTime.Today);

            var mondayToCompare = GetMonday(new DateTime(date.Year, date.Month, date.Day));
            var weeks = Math.Round((mondayToCompare - mondayThisWeek).TotalDays / 7);

            string response;
            if ((weeks % 2) == 1)
                response = ResourceResponse.SalDeFiesta;
            else
                response = ResourceResponse.SeResponsable;

            return response;
        }

        private DateTime GetMonday(DateTime dateSelected)
        {
            int diff = (7 + (dateSelected.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dateSelected.AddDays(-1 * diff).Date;
        }

        private SendModel CreateSend(string message)
        {
            var messageFullfillment = new List<SendMessage>();
            var msg = new List<string>();

            msg.Add(message);

            messageFullfillment.Add(new SendMessage
            {
                Text = new SendText
                {
                    TextList = msg
                }

            });

            return new SendModel()
            {
                FulfillmentMessagesList = messageFullfillment

            };
        }
    }
}
