<?php
    function wrap_error($err) {
        $styles = "\"margin-left: 10%;color: red;\"";
        return "<small style=".$styles.">".$err."</small>";
    }

    function restrict_unauthorized_access() {
        if (isset($_COOKIE['token'])) {
            $result = verify_token($_COOKIE['token']); 
            if ($result != null) {
                return $result;
            }
        }
        header("Location: http://almeswe-web-tech.com/sign-in.php");
        return null;
    }

    function remove_token($username, $token) {
        $mysqli = new mysqli('localhost', 'almeswe', 'root', 'web-tech-database');
        $result = $mysqli->query("UPDATE User SET session_token='-' WHERE username='$username' AND session_token='$token'");
        $mysqli->close();
    }

    function verify_token($token) {
        $mysqli = new mysqli('localhost', 'almeswe', 'root', 'web-tech-database');
        $result = $mysqli->query("SELECT * FROM User WHERE session_token='$token'");
        $mysqli->close();     
        if ($result->num_rows != 0) {
            return $result->fetch_assoc();
        }
        return null;
    }
?>