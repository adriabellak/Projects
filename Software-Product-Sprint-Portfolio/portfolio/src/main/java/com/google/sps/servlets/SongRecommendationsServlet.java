package com.google.sps.servlets;

import java.io.IOException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.gson.Gson;

import java.util.ArrayList; 

/** Handles requests sent to the /hello URL. Try running a server and navigating to /hello! */
@WebServlet("/songs")
public class SongRecommendationsServlet extends HttpServlet {

  @Override
  public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
    
    // Create ArrayList and add messages
    ArrayList<String> messages = new ArrayList<String>();
    messages.add("Man of War - Radiohead");
    messages.add("Codex - Radiohead");
    messages.add("Decks Dark - Radiohead");
    messages.add("Eternal Summer - The Strokes");
    messages.add("The Adults Are Talking - The Strokes");
    messages.add("Welcome to Japan - The Strokes");
    messages.add("At The Door - The Strokes");
    messages.add("Bad Decisions - The Strokes");
    messages.add("Disciples - Tame Impala");
    messages.add("Borderline - Tame Impala");
    messages.add("Taxi's Here - Tame Impala");
    messages.add("Posthumous Forgiveness - Tame Impala");
    messages.add("List Of People (To Try And Forget About) - Tame Impala");
    messages.add("Only in My Dreams - The Marias");
    messages.add("I Like It - The Marias");
    messages.add("I Don't Know You - The Marias");
    messages.add("Like You Do - Joji");
    messages.add("On Melancholy Hill - Gorillaz");
    messages.add("Imagination - Foster The People");
    messages.add("Social Cues - Cage The Elephant");
    messages.add("Tokyo Smoke - Cage The Elephant");
    messages.add("Sweetie Little Jean - Cage The Elephant");
    messages.add("Time in a Bottle - Jim Croce");

    // Convert to json using Gson
    Gson gson = new Gson();
    String json = gson.toJson(messages);

    //
    response.setContentType("application/json;");
    response.getWriter().println(json);
  }
}
