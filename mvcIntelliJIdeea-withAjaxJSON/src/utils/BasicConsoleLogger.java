package utils;

public class BasicConsoleLogger {

    public static void LogError(String errorMessage){
        System.out.println("\t[ERROR] " + errorMessage);
    }

    public static void LogInfo(String errorMessage){
        System.out.println("\t[INFO] " + errorMessage);
    }
}
