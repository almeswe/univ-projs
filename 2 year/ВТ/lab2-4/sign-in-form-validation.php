<?php
    $phone_error = "";
    $passw_error = "";

    function wrap_error($err) {
        $styles = "\"margin-left: 10%;color: red;\"";
        return "<small style=".$styles.">".$err."</small>";
    }

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        if (!isset($_POST['phone'])) {
            $phone_error = wrap_error("Phone field cannot be empty!");
        } 
        else {
            $phone = $_POST['phone'];
            $phone_pattern = '/^(((\+375[ ]?\((29|44)\))?)|((\((29|44)\))?))[ ]?[0-9]{3}([ -]?[0-9]{2}){2}+$/';
            if (!preg_match($phone_pattern, $phone)) {
                $phone_error = wrap_error("Phone pattern does not matches!");
            }
        }

        if (!isset($_POST['password'])) {
            $phone_error = "Password field cannot be empty!";
        }
        else {
            $passw = $_POST['password'];
            if (strlen($passw) <= 10) {
                $passw_error = wrap_error("Password should be greater than 10 symbols!");
            }
        }
    }
?>