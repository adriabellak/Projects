package main.java.com.google.sps.servlets;

import com.google.cloud.datastore.Datastore;
import com.google.cloud.datastore.DatastoreOptions;
import com.google.cloud.datastore.Entity;
import com.google.cloud.datastore.Query;
import com.google.cloud.datastore.QueryResults;
import com.google.cloud.datastore.StructuredQuery.OrderBy;
import com.google.gson.Gson;
import java.io.IOException;
import java.util.ArrayList;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/** Servlet responsible for listing food from all cities */
@WebServlet("/load-Chicago")
public class LoadChicago extends HttpServlet {

  @Override
  public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
    Datastore datastore = DatastoreOptions.getDefaultInstance().getService();
    Query<Entity> query =
        Query.newEntityQueryBuilder().setKind("Chicago").setOrderBy(OrderBy.desc("date")).build();
    QueryResults<Entity> results = datastore.run(query);

    ArrayList<String> entries = new ArrayList<>();
    while (results.hasNext()) {
        Entity entity = results.next();
        String date = entity.getString("date");
        String time = entity.getString("time");
        String address = entity.getString("address");
        String info = entity.getString("info");

        entries.add(date + " - " + time + " - " + address + " - " + info);
    }

    Gson gson = new Gson();

    response.setContentType("application/json;");
    response.getWriter().println(gson.toJson(entries));
  }
}