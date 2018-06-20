package controllers;

import domain.User;
import utils.BasicConsoleLogger;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.sql.SQLException;

public class LoginController extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

        String userName = request.getParameter("userName");
        String password = request.getParameter("password");
        RequestDispatcher requestDispatcher = null;

        try {
            DataBaseController dataBaseController = new DataBaseController();
            User user = dataBaseController.authenticate(userName, password);

            if(user != null){
                // add the user to the session, so we can use it later
                requestDispatcher = request.getRequestDispatcher("/main.jsp");
                HttpSession httpSession = request.getSession();
                httpSession.setAttribute("user", user);
            }
            else{
                requestDispatcher = request.getRequestDispatcher( "/error.jsp");
            }
        }
        catch (SQLException | ClassNotFoundException e) {
            BasicConsoleLogger.LogError(e.getMessage());
            e.printStackTrace();
        }

        if(requestDispatcher == null){
            BasicConsoleLogger.LogError("RequestDispatcher is null!");
            System.exit(1);
        }

        // finally, forward the request, be it error or succes
        requestDispatcher.forward(request, response);
    }
}
