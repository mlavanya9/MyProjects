//Services
weatherApp.service('cityService', function () {
    this.city = "New York, NY";
});
weatherApp.service('weatherService',[ '$resource', function ($resource) {
    this.GetWeather = function (city, days) {
        var weatherAPI = $resource("http://api.openweathermap.org/data/2.5/forecast?id=524901&APPID=a5cb0ae25df8a8a57600194642330c3f", {
            callback: "JSON_CALLBACK"
        }, { get: { method: "JSONP" } });
        return weatherAPI.get({
            q: city,
            cnt: days
        });
    };
   
}]);