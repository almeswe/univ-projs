<?php
    use PHPMailer\PHPMailer\PHPMailer;
    use PHPMailer\PHPMailer\Exception;

    function send_sign_in_notification($to) {
        require_once "PHPMailer/SMTP.php";
        require_once "PHPMailer/PHPMailer.php";
        require_once "PHPMailer/Exception.php";

        $mail = new PHPMailer();
        $mail->IsSMTP();
        $mail->Mailer = "smtp";

        $mail->SMTPAuth = true;
        $mail->SMTPSecure = "tls";
        $mail->Port = 587;
        $mail->Host = "smtp.gmail.com";
        $mail->Username = "almeswewebtech@gmail.com";
        $mail->Password = "webtech2085858";

        $mail->IsHTML(true);
        $mail->AddAddress($to, "recipient name");
        $mail->SetFrom($mail->Username, "almeswe webtech");
        $mail->Subject = "We noticed login to your account.";
        $host = $_SERVER['HTTP_HOST'];
        $usragent = $_SERVER['HTTP_USER_AGENT'];
        $content = 
        "<ol>
            <li>Host      : '$host'</li>
            <li>User-Agent: '$usragent'</li>
        </ol>";
        $mail->MsgHTML($content); 
        $mail->Send();
    }
?>