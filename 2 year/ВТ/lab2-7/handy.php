<?php
    require_once 'db.php';
    require_once 'vendor/autoload.php';
    require_once 'validation.php';

    Twig_Autoloader::register();

    $root = new Twig_Loader_Filesystem('.');
    $twig = new Twig_Environment($root);

    $auth_res = restrict_unauthorized_access();
    function render_header_for($twig, $page, $res) {
        $page_name = $page;
        $auth_res = $res;
        require_once 'header.php';
    }

    function render_home_page($twig, $auth_res) {
        $emulator = new Emulator();
        render_header_for($twig, "Home", $auth_res);
        $body_template = $twig->loadTemplate(
            'templates/home-template.html');
        $boxes = array();
        for ($i = 0; $i < 6; $i++) {
            $boxes[$i] = $emulator->get_track();
        }
        for ($i = 0; $i < 6; $i++) {
            $boxes[$i+6] = $emulator->get_album();
        }
        for ($i = 0; $i < 6; $i++) {
            $boxes[$i+12] = $emulator->get_artist();
        }
        echo $body_template->render(array(
            "boxes" => $boxes
        ));
    }

    function render_stream_page($twig, $auth_res) {
        $emulator = new Emulator();
        render_header_for($twig, "Stream", $auth_res);
        $body_template = $twig->loadTemplate(
            'templates/stream-template.html');
        $boxes = array();
        for ($i = 0; $i < 6; $i++) {
            $boxes[$i] = $emulator->get_track();
        }
        for ($i = 0; $i < 18; $i++) {
            $boxes[$i+6] = $emulator->get_track();
        }
        echo $body_template->render(array(
            "boxes" => $boxes
        ));
    }

    function render_library_page($twig, $auth_res) {
        $emulator = new Emulator();
        render_header_for($twig, "Library", $auth_res);
        $body_template = $twig->loadTemplate(
            'templates/library-template.html');
        $boxes = array();
        for ($i = 0; $i < 24; $i++) {
            $boxes[$i] = $emulator->get_track();
        }
        echo $body_template->render(array(
            "boxes" => $boxes
        ));
    }
?>