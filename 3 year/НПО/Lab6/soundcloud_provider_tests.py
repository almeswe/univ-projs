import unittest

from soundcloud_provider import *

class SouncloudProviderTest(unittest.TestCase):
    @classmethod
    def setUpClass(self) -> None:
        self._provider: SoundcloudProvider = \
            SoundcloudProvider(input('Enter username: '))
        super().setUpClass()
    
    @classmethod
    def tearDownClass(self) -> None:
        try:
            self._provider.quit()
        except Exception as e:
            pass
        super().tearDownClass()

    def test_followers_count(self) -> None:
        self.assertEqual(self._provider.get_actual_followers_count(),
            self._provider.get_followers_on_page())

    def test_following_count(self) -> None:
        self.assertEqual(self._provider.get_actual_following_count(),
            self._provider.get_following_on_page())

if __name__ == '__main__':
    unittest.main()