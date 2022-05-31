<!DOCTYPE html>
<html lang="en">
<head>
    <link rel="stylesheet" href="style.css">
    <title><?php $page_name ?></title>
</head>
<header>
    <div class="header-rect">        
        <div class="header-contents">
            <div class="header-entertainment-data">
                <a href="index.php" id="header-tab">
                    <div class="header-icon-tab" id="header-tab-container">
                        <img class="icon-image" src="imgs/icon.png">
                    </div>
                </a>
                <a href="index.php" id="header-tab" <?php if ($page_name == 'Home') echo 'class="header-tab-selected"'?>>
                    <div class="header-home-tab" id="header-tab-container">
                        <span id="header-tab-contents">Home</span>
                    </div>
                </a>
                <a href="stream.php" id="header-tab" <?php if ($page_name == 'Stream') echo 'class="header-tab-selected"'?>>
                    <div class="header-stream-tab" id="header-tab-container">
                        <span id="header-tab-contents">Stream</span>
                    </div>
                </a>
                <a href="library.php" id="header-tab" <?php if ($page_name == 'Library') echo 'class="header-tab-selected"'?>>
                    <div class="header-library-tab" id="header-tab-edge-container">
                        <span id="header-tab-contents">Library</span>
                    </div>
                </a>
            </div>
            <?php
            if ($auth_res != null) {
                $username = $auth_res["username"];
                echo
                '<div class="header-authentication-data">
                    <a href="index.php" id="header-tab">
                        <div id="header-tab-container">
                            <span id="header-tab-contents">'.$username.'</span>
                        </div>
                    </a>
                    <a href="sign-out.php" id="header-tab">
                        <div id="header-tab-edge-container">
                            <span id="header-tab-contents">sign out</span>
                        </div>
                    </a>
                </div>';
            } else {
                echo 
                '<div class="header-authentication-data">
                    <a href="sign-in.php" id="header-tab">
                        <div id="header-tab-container">
                            <span id="header-tab-contents">sign in</span>
                        </div>
                    </a>
                    <a href="register.php" id="header-tab">
                        <div id="header-tab-edge-container">
                            <span id="header-tab-contents">register</span>
                        </div>
                    </a>
                </div>';
            }
            ?>
        </div>
    </div>       
</header>