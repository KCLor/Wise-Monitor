from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from selenium.common.exceptions import TimeoutException, WebDriverException
import time
import firebase_admin
from firebase_admin import credentials
from firebase_admin import db

cred_obj = firebase_admin.credentials.Certificate('firebase-sdk.json')
firebase_admin.initialize_app(cred_obj, {
	'databaseURL': 'https://wise-monitor-price-default-rtdb.asia-southeast1.firebasedatabase.app/'
	})

chrome_path = r"C:\Users\gundu\PycharmProjects\pythonProject\drivers\chromedriver.exe"
chrome_options = webdriver.ChromeOptions()
chrome_options.add_argument("--incognito")
browser = webdriver.Chrome(chrome_path, options=chrome_options)
ref = db.reference('/')
ref.child('JayaGrocer').delete()

# method for finding product name and price
def getitem():
    product = browser.find_elements_by_xpath('.//*[@class="product-item product-item--vertical  1/3--tablet 1/4--lap-and-up"]')

    for post in product:
        name = post.find_element_by_xpath('.//*[@class="product-item__title text--strong link"]').text
        price = post.find_element_by_class_name('price').text
        ref = db.reference('JayaGrocer')
        emp_ref = ref.push({
                     'name': name,
                     'price': price
                })

# delete the # in the list below to retrieve the products' name and price from the link
url = {#'https://jggp.jayagrocer.com/collections/bakery',
    #'https://jggp.jayagrocer.com/collections/fresh-market',
    #'https://jggp.jayagrocer.com/collections/chilled-and-frozen',
    #'https://jggp.jayagrocer.com/collections/food-essentials',
    #'https://jggp.jayagrocer.com/collections/beverages',
    #'https://jggp.jayagrocer.com/collections/baby-care',
    #'https://jggp.jayagrocer.com/collections/beauty-and-health',
    #'https://jggp.jayagrocer.com/collections/household-products',
    #'https://jggp.jayagrocer.com/collections/pet-shop'
    'https://jggp.jayagrocer.com/collections/coffee-and-tea'
}

for link in url:
    browser.get(link)
    browser.maximize_window()
    print(link)
    getitem()     # scrap first page
    while True:   # move to next page
        try:
            browser.execute_script("return arguments[0].scrollIntoView(true);",WebDriverWait(browser, 10).until(
             EC.presence_of_element_located((By.XPATH, "//a[@title = 'Next']"))
                 ))
            browser.find_element_by_xpath("//a[@title = 'Next']").click()
            time.sleep(3)
            # element.click()
            print("------------------Next Page-----------------")
            # scraping the following pages
            getitem()
        except (TimeoutException, WebDriverException) as e:
            print("Last page reached")
            break

