# WeatherChecker
 Full Stack Weather Checker API & UI Programming Challenge Interview


## Testing
To run the test suite, cd to WeatherCheckerTests and run `dotnet test`

## Running
To run the Back End, `cd` to WeatherCheckerAPI and run `dotnet run`

## Configuration
You can change various settings in `WeatherCheckerAPI\appsettings.json` such as API Keys and Usage Limits.

## Usage
Once the Back End is running you can access it at [http://localhost:5136/swagger.html](http://localhost:5136/swagger/index.html) to see the Swagger Generated Documentation
You can access the front end by opening the file `.\WeatherCheckerUI\index.html`
You'll need to enter a City Name (i.e. Melbourne), a Country Name code (I.e. AU), and a valid API Key (See appsettings.json for a list of valid API Keys)
Click Check Weather and the Description from that City's OpenWeather Data will show below the Check Weather button.
The default usage limit for each API Key is 5 uses per hour.