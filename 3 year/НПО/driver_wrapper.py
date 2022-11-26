import time

from typing import List

from selenium.webdriver import Chrome
from selenium.webdriver import ChromeOptions

from selenium.webdriver.common.by import By
from selenium.webdriver.remote.webelement import WebElement

from selenium.webdriver.common.keys import Keys

from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as ec

class DriverWrapper(Chrome): 
    def __init__(self) -> None:
        super().__init__(executable_path='./chromedriver',
            options=self._init_options())
        self._init_waiter()

    def _init_waiter(self) -> None:
        self.waiter: WebDriverWait = WebDriverWait(self, 5.0)

    def _init_options(self) -> ChromeOptions:
        options: ChromeOptions = ChromeOptions()
        return options

    def scroll_until_appears(self, by: By, value: str, delay: float = 1.0) -> None:
        current: int = 0
        previous: int = 0
        attempt: int = 1
        attempts: int = 5
        root: WebElement = self.find_element(By.TAG_NAME, 'html') 
        while attempt <= attempts:
            #self.execute_script("window.scrollTo(0, document.body.scrollHeight)") 
            root.send_keys(Keys.PAGE_DOWN)
            items: List[WebElement] = self.find_elements(by, value)
            current = len(items)
            if current > previous:
                previous = current
                attempt = 1
            else:
                attempt += 1
            time.sleep(delay)

if __name__ == '__main__':
    print('try to run main.py')