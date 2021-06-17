package com.google.sps.servlets;

import com.google.gson.Gson;
import java.util.ArrayList;
import java.io.IOException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/** Handles requests sent to the /hello URL. Try running a server and navigating to /hello! */
@WebServlet("/weather")
public class WeatherServlet extends HttpServlet {

    public class Weather {
        String weather;
        String season;
        public Weather(String weather_event, String name)
        {
            weather = weather_event;
            season = name;
        }
    }

  @Override
  public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
    ArrayList<Weather> phenomena = new ArrayList<Weather>();

    Weather blizzard = new Weather("blizzards", "winter");
    Weather supercell = new Weather("tornadoes", "spring");
    Weather eye = new Weather("hurricanes", "fall");
    Weather derecho = new Weather("destructive thunderstorms", "summer");

    phenomena.add(blizzard);
    phenomena.add(supercell);
    phenomena.add(eye);
    phenomena.add(derecho);

    String result = convertToJson(phenomena);

    // Send the JSON as the response
    response.setContentType("application/json;");
    response.getWriter().println(result);
  }

  /**
   * Converts a ServerStats instance into a JSON string using the Gson library. Note: We first added
   * the Gson library dependency to pom.xml.
   */
  private String convertToJson(ArrayList<Weather> phenomena) {
    Gson gson = new Gson();
    String json = gson.toJson(phenomena);
    return json;
  }
}

