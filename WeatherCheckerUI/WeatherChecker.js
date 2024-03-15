document.getElementById("weatherForm").addEventListener("submit", async function(event){
    event.preventDefault();

    const city = document.getElementById("cityInput").value;
    const country = document.getElementById("countryInput").value;
    const apiKey = document.getElementById("apiInput").value;

    try {
        const response = await fetch(`http://127.0.0.1:5136/api/WeatherChecker/${city}/${country}?apiKey=${apiKey}`);
        document.getElementById("weatherResult").innerText = await response.text();
    } catch (error) {
        console.error("Error fetchubng weather data: ", error);
        document.getElementById("weatherResult").innerText = "Error fetching Weather data.";
    }
})