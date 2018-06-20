package controllers;

import domain.User;

import java.sql.*;

public class DataBaseController {

    private String m_connectionString = "jdbc:mysql://localhost:3306/lab_8";
    private String m_databaseUser = "root";
    private String m_password = "";
    private String m_mysqlDriverName = "org.gjt.mm.mysql.Driver";
    //"org.gjt.mm.mysql.Driver";
    //"com.mysql.jdbc.Driver

    private String m_userNameKey = "Name";
    private String m_userIdKey = "UserId";
    private String m_passwordKey = "Password";

    private Statement m_statement;

    public DataBaseController() throws SQLException, ClassNotFoundException {

        connectToDatabase();
    }


    public void connectToDatabase() throws ClassNotFoundException, SQLException {

        Class.forName(m_mysqlDriverName);
        Connection connection = DriverManager.getConnection(m_connectionString, m_databaseUser, m_password);
        m_statement = connection.createStatement();
    }

    public User authenticate(String userName, String password) throws SQLException {

        User user = null;
        ResultSet resultSet;

        resultSet = m_statement.executeQuery(
                "SELECT * FROM users WHERE Name = '" + userName + "' AND Password = '" + password + "'");

        // move the cursor one position forward, since by default it is at -1
        if(resultSet.next()){
            // now we know we have a match
            user = new User(resultSet.getInt(m_userIdKey), resultSet.getString(m_userNameKey), resultSet.getString(m_passwordKey));
        }

        resultSet.close();
        return user;
    }


}
