package domain;

public class User {
    private int UserId;
    private String Name;
    private String Ppassword;

    public void setUserId(int userId) {
        UserId = userId;
    }

    public void setName(String name) {
        Name = name;
    }

    public void setPpassword(String ppassword) {
        Ppassword = ppassword;
    }

    public int getUserId() {

        return UserId;
    }

    public String getName() {
        return Name;
    }

    public String getPpassword() {
        return Ppassword;
    }

    public User(int userId, String name, String password) {
        UserId = userId;
        Name = name;
        Ppassword = password;
    }
}
