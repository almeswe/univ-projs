<?php
    require_once "validation.php";
    $credentials_error = '';
    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        require_once "mysql.php";
        $result = get_by_credentials($mysqli, $_POST['username'], $_POST['password']);
        if ($result->num_rows != 0) {
            require_once "notifications.php";
            set_appropriate_cookies($mysqli, $_POST['username'], $_POST['password']);
            send_sign_in_notification($result->fetch_assoc()['email']);
            header("Location: http://almeswe-web-tech.com/index.php");
        }
        else {
            $credentials_error = wrap_error("Incorrect credentials specified!");
        }
        $mysqli->close();
    }
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <link rel="stylesheet" href="style-form.css">
    <title>Sign in</title>
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
                <a href="register.php" id="header-tab">
                    <div id="header-tab-edge-container">
                        <span id="header-tab-contents">register</span>
                    </div>
                </a>
            </div>
            </div>
        </div>       
    </header>    
    <div class="contents-rect">
        <div class="form-rect">
            <div class=form-container>
                <h2 class="form-main-text">Sign in</h2>
                <form class="sign-in-form" method="post">
                    <ul type="none" class="form-credentials-box">
                        <li class="form-credentials-box-item">
                            <label for="username-tb">Username:</label>  
                            <input id="username-tb" type="text" name="username">
                            <?php echo $credentials_error ?>
                        </li>
                        <li class="form-credentials-box-item">
                            <label for="password-tb">Password:</label>
                            <input id="password-tb" type="password" name="password">
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