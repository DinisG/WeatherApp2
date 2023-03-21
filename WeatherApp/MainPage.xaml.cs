using System;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
	RestService _restService;
    Dictionary<string, string> weatherImg = new Dictionary<string, string>();

    


    public MainPage()
	{
		InitializeComponent();
		_restService = new RestService();
        weatherImg.Add("01d", "https://openweathermap.org/img/wn/01d.png");
        weatherImg.Add("02d", "https://openweathermap.org/img/wn/02d.png");
        weatherImg.Add("03d", "https://openweathermap.org/img/wn/03d.png");
        weatherImg.Add("04d", "https://openweathermap.org/img/wn/04d.png");
        weatherImg.Add("05d", "https://openweathermap.org/img/wn/05d.png");
        weatherImg.Add("06d", "https://openweathermap.org/img/wn/06d.png");
        weatherImg.Add("07d", "https://openweathermap.org/img/wn/07d.png");
        weatherImg.Add("08d", "https://openweathermap.org/img/wn/08d.png");
        weatherImg.Add("09d", "https://openweathermap.org/img/wn/09d.png");
        weatherImg.Add("10d", "https://openweathermap.org/img/wn/10d.png");
        weatherImg.Add("11d", "https://openweathermap.org/img/wn/11d.png");
        weatherImg.Add("12d", "https://openweathermap.org/img/wn/12d.png");
        weatherImg.Add("13d", "https://openweathermap.org/img/wn/13d.png");
        weatherImg.Add("50d", "https://openweathermap.org/img/wn/50d.png");
        weatherImg.Add("01n", "https://openweathermap.org/img/wn/01n.png");
        weatherImg.Add("02n", "https://openweathermap.org/img/wn/02n.png");
        weatherImg.Add("03n", "https://openweathermap.org/img/wn/03n.png");
        weatherImg.Add("04n", "https://openweathermap.org/img/wn/04n.png");
        weatherImg.Add("05n", "https://openweathermap.org/img/wn/05n.png");
        weatherImg.Add("06n", "https://openweathermap.org/img/wn/06n.png");
        weatherImg.Add("07n", "https://openweathermap.org/img/wn/07n.png");
        weatherImg.Add("08n", "https://openweathermap.org/img/wn/08n.png");
        weatherImg.Add("09n", "https://openweathermap.org/img/wn/09n.png");
        weatherImg.Add("10n", "https://openweathermap.org/img/wn/10n.png");
        weatherImg.Add("11n", "https://openweathermap.org/img/wn/11n.png");
        weatherImg.Add("12n", "https://openweathermap.org/img/wn/12n.png");
        weatherImg.Add("13n", "https://openweathermap.org/img/wn/13n.png");
        weatherImg.Add("50n", "https://openweathermap.org/img/wn/50n.png");


        WeatherData weatherData = new();
        weatherData.IconUrl = Constants.DefaultImg;
        BindingContext = weatherData;

    }

    async void OnGetWeatherButtonClicked(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
		{
            WeatherData weatherData = new();
            try 
            {  
                weatherData = await _restService.GetWeatherData(GenerateRequestURL(Constants.OpenWeatherMapEndpoint));
                if (weatherData is null)
                {
                    weatherData = new();
                    weatherData.IconUrl = Constants.DefaultImg;
                }
                else 
                {
                    string value;
                    if (weatherImg.TryGetValue(weatherData.Weather[0].Icon, out value))
                    {
                        var iconUrl = weatherImg[weatherData.Weather[0].Icon];
                        weatherData.IconUrl = iconUrl;
                    }
                    else
                    {
                        weatherData.IconUrl = Constants.DefaultImg;
                    }
                }
            }
            catch(HttpRequestException)
            {
                weatherData.IconUrl = Constants.DefaultImg; 
            }
            catch(InvalidOperationException)
            {
                weatherData.IconUrl = Constants.DefaultImg;
            }
            catch (TaskCanceledException)
            {
                weatherData.IconUrl = Constants.DefaultImg;
            }
            catch (UriFormatException)
            {
                weatherData.IconUrl = Constants.DefaultImg;
            }
            BindingContext = weatherData;
        }

		
	}

	string GenerateRequestURL(string endPoint)
	{
		string requestUri = endPoint;
		requestUri += $"?q={_cityEntry.Text}";
		requestUri += $"&units=metric";
		requestUri += $"&appid={Constants.OpenWeatherMapAPIKey}";
		return requestUri;
	}		
}

