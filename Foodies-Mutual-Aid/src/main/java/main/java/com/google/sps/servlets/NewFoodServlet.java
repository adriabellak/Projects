package main.java.com.google.sps.servlets;

import com.google.cloud.datastore.Datastore;
import com.google.cloud.datastore.DatastoreOptions;
import com.google.cloud.datastore.Entity;
import com.google.cloud.datastore.FullEntity;
import com.google.cloud.datastore.KeyFactory;
import java.io.IOException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
// import org.jsoup.Jsoup;
// import org.jsoup.safety.Whitelist;

/** Servlet responsible for creating new tasks. */
@WebServlet("/new-food")
public class NewFoodServlet extends HttpServlet {

  @Override
  public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {

    // Get the value entered in the form.
    String date = request.getParameter("date-input");
    String time = request.getParameter("time-input");
    String city = request.getParameter("city-input");
    String address = request.getParameter("address-input");
    String info = request.getParameter("info-input");

    Datastore datastore = DatastoreOptions.getDefaultInstance().getService();
    KeyFactory keyFactory = datastore.newKeyFactory().setKind(city);
    FullEntity foodEntity =
        Entity.newBuilder(keyFactory.newKey())
            .set("date", date)
            .set("time", time)
            .set("city", city)
            .set("address", address)
            .set("info", info)
            .build();
    datastore.put(foodEntity);

    response.sendRedirect("/submit.html");
  }
}