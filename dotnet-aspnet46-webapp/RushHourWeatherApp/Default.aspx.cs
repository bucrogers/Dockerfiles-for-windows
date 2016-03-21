using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace WebAppForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorCtl.Text = string.Empty;

            UpdateFromService(this).Wait();
        }

        protected void ctrlRefresh_Click(object sender, EventArgs e)
        {
            UpdateFromService(this).Wait();
        }

        static async Task UpdateFromService(_Default pageContext)
        {

            var serviceUrl = Properties.Settings.Default.WeatherServiceUrl;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serviceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // pm request
                    {
                        HttpResponseMessage response = client.GetAsync("today/amrush").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var weatherData = await response.Content.ReadAsAsync<WeatherData>();

                            var firstSample = weatherData.samples[0];
                            pageContext.amTempCtl.Text = firstSample.temperatureF.ToString();
                            pageContext.amWindCtl.Text = firstSample.windSpeedMph + "MPH "
                                    + firstSample.windDirection;
                            pageContext.amPrecipCtl.Text = firstSample.precipitation;
                        }
                    }

                    // pm request
                    {
                        HttpResponseMessage response = client.GetAsync("today/pmrush").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var weatherData = await response.Content.ReadAsAsync<WeatherData>();

                            var firstSample = weatherData.samples[0];
                            pageContext.pmTempCtl.Text = firstSample.temperatureF.ToString();
                            pageContext.pmWindCtl.Text = firstSample.windSpeedMph + "MPH "
                                    + firstSample.windDirection;
                            pageContext.pmPrecipCtl.Text = firstSample.precipitation;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pageContext.errorCtl.Text = "Error using url '" + serviceUrl + "': " + ex.Message;
            }
        }
    }
}