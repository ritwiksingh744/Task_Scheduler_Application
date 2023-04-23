function LoadWeather() {

    function CallWeatherApi() {
		$.ajax({
			type: 'GET',
			url: 'https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Chandigarh%2C%20India?unitGroup=metric&key=ADWV7PY62GV8G7ML4HDZZ8Y6E&contentType=json',
			success: function (response) {
				console.log("weather response: ", response);
			},
			error: function (err) {
				console.log("Error: ", err.Message);
            }
		});
    }

    function onInit() {

    }

    return {
		onInit: onInit,
		CallWeatherApi: CallWeatherApi
    }
}