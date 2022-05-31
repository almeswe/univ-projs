<?php 
    require_once "validation.php";
    $result = verify_token($_COOKIE['token']);
    if ($result != null) {
        remove_token($result['username'], $_COOKIE['token']);
        unset($_COOKIE['token']);
    }
    header("Location: http://almeswe-web-tech.com/sign-in.php");
?>