<?php
    require_once "validation.php";

    $email_error = "";
    $passw_error = "";
    $username_error = "";
    $is_errored = false;

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        if (!isset($_POST['username'])) {
            $username_error = "Username field cannot be empty!";
            $is_errored = true;
        }
        else {
            $username = $_POST['username'];
            $username_pattern = "/^[a-zA-Z_]([a-zA-Z_0-9]*)$/";
            if (!preg_match($username_pattern, $username)) {
                $username_error = wrap_error("Username pattern does not matches!");
                $is_errored = true;
            }
            else {
                $length = strlen($username);
                if (($length < 4) || ($length > 32)) {
                    $username_error = wrap_error("Size must be in range [4, 32]!");
                    $is_errored = true;
                }
            }
        }

        if (!isset($_POST['email'])) {
            $email_error = wrap_error("Email field cannot be empty!");
            $is_errored = true;
        } 
        else {
            $email = $_POST['email'];
            $email_pattern = '/^[a-zA-Z][a-zA-Z0-9]*\@[a-zA-Z]+\.[a-z]+$/';
            if (!preg_match($email_pattern, $email)) {
                $email_error = wrap_error("Email pattern does not matches!");
                $is_errored = true;
            }
        }

        if (!isset($_POST['password'])) {
            $passw_error = "Password field cannot be empty!";
            $is_errored = true;
        }
        else {
            $passw = $_POST['password'];
            if (strlen($passw) <= 5) {
                $passw_error = wrap_error("Password should be greater than 10 symbols!");
                $is_errored = true;
            }
        }
        if (!$is_errored) {
            require_once "mysql.php";
            $email = $_POST['email'];
            $password = $_POST['password'];
            $username = $_POST['username'];
            $result = get_users($mysqli, $username, $email);
            if ($result->num_rows != 0) {
                $result = $result->fetch_assoc();
                if ($result['username'] == $username) {
                    $username_error = wrap_error("Username is registered.");
                }
                if ($result['email'] == $email) {
                    $email_error = wrap_error("Email is registered.");
                }
            }
            else {
                if (add_new_user($mysqli, $username, $email, $password)) {
                    header("Location: http://almeswe-web-tech.com/index.php");
                }
            }
            $mysqli->close();
        }
    }
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <link rel="stylesheet" href="style-form.css">
    <title>Register</title>
</head>
<body>
    <header>
        <div class="header-rect">        
            <div class="header-contents">
                <div class="header-entertainment-data">
                    <a href="index.php" id="header-tab">
                        <div class="header-icon-tab" id="header-tab-container">
                            <img class="icon-image" src="imgs/icon.png">
                        </div>
                    </a>
                    <a href="index.php" id="header-tab">
                        <div class="header-home-tab" id="header-tab-container">
                            <span id="header-tab-contents">Home</span>
                        </div>
                    </a>
                    <a href="stream.php" id="header-tab">
                        <div class="header-stream-tab" id="header-tab-container">
                            <span id="header-tab-contents">Stream</span>
                        </div>
                    </a>
                    <a href="library.php" id="header-tab">
                        <div class="header-library-tab" id="header-tab-edge-container">
                            <span id="header-tab-contents">Library</span>
                        </div>
                    </a>
                </div>
            <div class="header-authentication-data">
                <a href="sign-in.php" id="header-tab">
                    <div id="header-tab-edge-container">
                        <span id="header-tab-contents">sign in</span>
                    </div>
                </a>
            </div>
            </div>
        </div>       
    </header>    
    <div class="contents-rect">
        <div class="form-rect">
            <div class=form-container>
                <h2 class="form-main-text">Register</h2>
                <form class="sign-in-form" method="post">
                    <ul type="none" class="form-credentials-box">
                        <li class="form-credentials-box-item">
                            <label for="username-tb">Username:</label>  
                            <input id="username-tb" type="text" name="username">
                            <?php echo $username_error ?>
                        </li>
                        <li class="form-credentials-box-item">
                            <label for="email-tb">Email:</label>  
                            <input id="email-tb" type="text" name="email">
                            <?php echo $email_error ?>
                        </li>
                        <li class="form-credentials-box-item">
                            <label for="password-tb">Password:</label>
                            <input id="password-tb" type="password" name="password">
                            <?php echo $passw_error ?>
                        </li>
                        <li class="form-credentials-box-item">
                            <button class="sign-in-button">Sign in</button>
                        </li>
                    </ul>
                </form>
            </div>
        </div>
    </div>
</body>
</html>