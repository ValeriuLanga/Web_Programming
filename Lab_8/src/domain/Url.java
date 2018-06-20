package domain;

public class Url {
    private int UrlId;
    private String Url;
    private int UserId;

    public void setUrlId(int urlId) {
        UrlId = urlId;
    }

    public void setUrl(String url) {
        Url = url;
    }

    public void setUserId(int userId) {
        UserId = userId;
    }

    public int getUrlId() {

        return UrlId;
    }

    public String getUrl() {
        return Url;
    }

    public int getUserId() {
        return UserId;
    }

    public Url(int urlId, String url, int userId) {

        UrlId = urlId;
        Url = url;
        UserId = userId;
    }
}
