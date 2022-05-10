using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System.IO;
using Newtonsoft.Json;
using System;
using ChusyKidsAPI.Resources;
using ChusyKidsAPI.Model;
using static Google.Cloud.Dialogflow.V2.Intent.Types.Message.Types;
using static Google.Cloud.Dialogflow.V2.Intent.Types.Message.Types.BasicCard.Types;
using static Google.Cloud.Dialogflow.V2.Intent.Types.Message.Types.BasicCard.Types.Button.Types;

namespace ChusyKidsAPI.Controllers
{
    [Route("webhook")]
	public class WebhookController : Controller
	{
		private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

		[HttpPost]
		public async Task<JsonResult> GetWebhookResponse()
		{
			WebhookRequest request;
			using (var reader = new StreamReader(Request.Body))
			{
				request = jsonParser.Parse<WebhookRequest>(reader);
			}

			var pas = request.QueryResult.Parameters;

			ReciveModel data = JsonConvert.DeserializeObject<ReciveModel>(request.QueryResult.ToString());

			var resultResponse = LogicKids(data.parameters.date);

			var response = (new WebhookResponse()).FulfillmentText = resultResponse;

            //response = WebhookWithTarget(resultResponse);

			return Json(response);
		}

		private string LogicKids(DateTime date)
		{
			var mondayThisWeek = GetMonday(DateTime.Today);

			var mondayToCompare = GetMonday(new DateTime(date.Year, date.Month, date.Day));
			var weeks = Math.Round((mondayToCompare - mondayThisWeek).TotalDays / 7);

			string response;
			if ((weeks % 2) == 1)
				response = ResourceResponse.SeResponsable;
			else
				response = ResourceResponse.SalDeFiesta;

			return response;
		}

		private DateTime GetMonday(DateTime dateSelected)
		{
			int diff = (7 + (dateSelected.DayOfWeek - DayOfWeek.Monday)) % 7;
			return dateSelected.AddDays(-1 * diff).Date;
		}

        private WebhookResponse WebhookWithTarget(string fulfillmentText)
        {
            return new WebhookResponse
            {
                FulfillmentText = fulfillmentText,
                FulfillmentMessages =
                {
                    new Google.Cloud.Dialogflow.V2.Intent.Types.Message
                    {
                        SimpleResponses = new SimpleResponses
                        { SimpleResponses_ =
                            {
                                new SimpleResponse
                                {
                                    DisplayText = "Text Simple Response",
                                    TextToSpeech = "The speech",
                                    Ssml = "<speak>The speech</speak>"
                                }
                            }
                        }
                    },
                    new Google.Cloud.Dialogflow.V2.Intent.Types.Message
                    {
                        BasicCard = new BasicCard
                        {
                            Title = "TITULO DE LA TARJETA",
                            Subtitle = "Subtitulo de la tarjeta",
                            FormattedText = "Esto es la descripción de la tarjeta, estoy rellenandolo a ver que pasa",
                            Buttons =
                            {
                                new Button
                                {
                                    Title = "TITULO DEL BOTOÓN",
                                    OpenUriAction = new OpenUriAction
                                    {
                                        Uri = "https://web.telegram.org/k/"
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

    }
}
