<?php
    require_once 'db.php';
    require_once 'vendor/autoload.php';

    Twig_Autoloader::register();

    $root = new Twig_Loader_Filesystem('.');
    $twig = new Twig_Environment($root);

    function render_header_for($twig, $page) {
        $header_template = $twig->loadTemplate(
            'templates/header-template.html');
        echo $header_template->render(array(
            "page" => $page
        ));
    }

    function render_home_page($twig) {
        $emulator = new Emulator();
        render_header_for($twig, "Home");
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

    function render_stream_page($twig) {
        $emulator = new Emulator();
        render_header_for($twig, "Stream");
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

    function render_library_page($twig) {
        $emulator = new Emulator();
        render_header_for($twig, "Library");
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