<!DOCTYPE html>
<html lang="en">
<head>
    <?php require "sign-in-form-validation.php"?>
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
            </div>
        </div>       
    </header>    
    <div class="contents-rect">
        <div class="form-rect">
            <div class=form-container>
                <h2 class="form-sign-in-text">Sign in</h2>
                <form class="sign-in-form" method="post">
                    <ul type="none" class="form-credentials-box">
                        <li class="form-credentials-box-item">
                            <label for="phone-tb">Phone:</label>  
                            <input id="phone-tb" type="text" name="phone">
                            <?php echo $phone_error ?>
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