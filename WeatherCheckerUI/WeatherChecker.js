function checkWeather() {
    var cityName = document.getElementById("cityInput").value;
    var countryName = document.getElementById("countryInput").value;
    //apiKey = 

    if (!cityName || !countryName) {
        alert("Please enter both a city and country name");
        return;
    }

    $.ajax({
        URL: "http://localhost:5136/api/WeatherChecker/" + encodeURIComponent(cityName) + "/" + encodeURIComponent(countryName),
        method: "GET",
        success: function(response) {
            $("#weatherData").html(response);
        },
        error: function(xhr, status, error) {
            var errorMessage = xhr.responseText || "An error occured while fetching Weather Data";
            $("#weatherData").html("Error: " + errorMessage);
        }
    })
}