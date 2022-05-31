<?php
    class Emulator {
        private $tracks_index = 0;
        private $albums_index = 0;
        private $artists_index = 0;

        private $tracks = array();
        private $albums = array();
        private $artists = array();

        public function __construct() {
            $temp = $this->scandir_clear('imgs/tracks/');
            shuffle($temp);
            $this->tracks = $temp;
            $temp = $this->scandir_clear('imgs/albums/');
            shuffle($temp);
            $this->albums = $temp;
            $temp = $this->scandir_clear('imgs/artists/');
            shuffle($temp);
            $this->artists = $temp;
        }
    
        public function get_track() {
            return $this->tracks[$this->tracks_index++ % 
                (count($this->tracks))];
        }

        public function get_album() {
            return $this->albums[$this->albums_index++ % 
                (count($this->albums))];
        }

        public function get_artist() {
            return $this->artists[$this->artists_index++ % 
                (count($this->artists))];
        }

        private function scandir_clear($path) {
            $index = 0;
            $abs_files = array();
            $files = scandir($path);
            for ($i = 0; $i < count($files); $i++) {
                if ($files[$i] != '.' and $files[$i] != '..') {
                    $abs_files[$index++] = $path . $files[$i];
                }
            }
            return $abs_files;
        }
    }
?>