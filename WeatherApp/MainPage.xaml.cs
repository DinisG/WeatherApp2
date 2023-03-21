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

