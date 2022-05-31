<?php
    $mysqli = new mysqli('localhost', 'almeswe',
        'root', 'web-tech-database');
    if ($mysqli->connect_errno) {
        printf("Connect failed: %s\n", $mysqli->connect_error);
        exit();
    }

    function get_users($mysqli, $username, $email) {
        $result = $mysqli->query("SELECT * FROM User WHERE username='$username' OR email='$email'");
        return $result;
    }

    function get_by_credentials($mysqli, $username, $password) {
        $result = $mysqli->query("SELECT * FROM User WHERE username='$username' AND password='$password'");
        return $result;
    }

    function add_new_user($mysqli, $username, $email, $password) {
        $result = $mysqli->query("INSERT INTO User (username, email, password, session_token) VALUES ('$username', '$email', '$password', '-')");
        return $result;
    }

    function set_appropriate_cookies($mysqli, $username, $password) {
        $hashed = hash('ripemd160', $password);
        $result = $mysqli->query("UPDATE User SET session_token='$hashed' WHERE username='$username'");
        if ($result == true) {
            setcookie('token', $hashed);
        }
    }
?>