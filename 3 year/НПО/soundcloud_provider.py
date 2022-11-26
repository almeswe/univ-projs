from driver_wrapper import *

class SoundcloudProvider(DriverWrapper):
    def __init__(self, user: str) -> None:
        super().__init__()
        self._init_urls(user)
        self._init_stats()

    def _init_urls(self, user: str) -> None:
        self._user: str = user
        self._url = f"https://soundcloud.com/{self._user}"
        self._followers_url: str = f"{self._url}/followers"
        self._following_url: str = f"{self._url}/following"

    def _init_stats(self) -> None:
        try:
            self.get(self._url)
            try:
                self.accept_button_click()
                self.waiter.until(ec.presence_of_element_located(
                    (By.XPATH, '//article[@class="infoStats"]')))
                article: WebElement = self.find_element(
                    By.XPATH, '//article[@class="infoStats"]')
                data: List[str] = article.text.split('\n')
                self._followers: int = data[1]
                self._following: int = data[3]
                print(f"Initialized for profile: {self._user}")
                print(f"\tmain     : {self._url}")
                print(f"\tfollowers: {self._followers_url}")
                print(f"\tfollowing: {self._following_url}")
                print(f"followers: {self._followers}, following: {self._following}")
            except Exception as e:
                print(f'Error occured while parsing stats: {e}')
                exit(1)
        except Exception as e:
            print(f'Error occured while navigating: {e}')
            exit(1)

    def accept_button_click(self) -> None:
        try:
            self.waiter.until(ec.element_to_be_clickable(
                (By.ID, 'onetrust-accept-btn-handler')))
            accept: WebElement = self.find_element(By.ID, 'onetrust-accept-btn-handler')
            if accept != None:
                accept.click()
        except Exception as e:
            pass
            
    def get_followers_on_page(self) -> int:
        return int(self._followers)

    def get_following_on_page(self) -> int:
        return int(self._following)

    def get_actual_followers_count(self) -> int:
        self.get(self._followers_url)
        self.scroll_until_appears(By.XPATH, '//li[@class="badgeList__item"]')
        return len(self.find_elements(By.XPATH, '//li[@class="badgeList__item"]'))

    def get_actual_following_count(self) -> int:
        self.get(self._following_url)
        self.scroll_until_appears(By.XPATH, '//li[@class="badgeList__item"]')
        return len(self.find_elements(By.XPATH, '//li[@class="badgeList__item"]'))

if __name__ == '__main__':
    print('try to run main.py')