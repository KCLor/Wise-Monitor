from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from selenium.common.exceptions import TimeoutException, WebDriverException
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
ref.child('Pacific').delete()

# method for finding product name and price
def findItem():
    product = browser.find_elements_by_xpath(
        './/*[@class="product_grid-item grid__item product_img-crop ratio_1-1 small--one-half medium--one-half large--one-quarter"]')
    product2 = browser.find_elements_by_xpath(
        './/*[@class="product_grid-item grid__item product_img-crop ratio_1-1 small--one-half medium--one-half large--one-quarter on-sale"]')

    for post in product:
        name = post.find_element_by_xpath('.//*[@class="grid-link__title"]').text
        price = post.find_element_by_xpath('.//*[@data-currency="MYR"]').text

        ref = db.reference('Pacific')
        emp_ref = ref.push({
                        'name': name,
                        'price': price
            })

    for post2 in product2:
        name2 = post2.find_element_by_xpath('.//*[@class="grid-link__title"]').text
        price2 = post2.find_element_by_xpath('.//*[@data-currency="MYR"]').text
        ref = db.reference('Pacific')
        emp_ref = ref.push({
            'name': name2,
            'price': price2
            })

    print("new data created")

# delete the # in the list below to retrieve the products' name and price from the link
url = {#'https://www.pacific-online.com.my/collections/groceries',
#'https://www.pacific-online.com.my/collections/toiletries',
#'https://www.pacific-online.com.my/collections/wet-tissues-diapers-napkin',
#'https://www.pacific-online.com.my/collections/beauty',
#'https://www.pacific-online.com.my/collections/toiletries-health-beauty',
#'https://www.pacific-online.com.my/collections/household-1',
#'https://www.pacific-online.com.my/collections/pets-care',
'https://www.pacific-online.com.my/collections/beverages/coffee'}
for link in url:
    browser.get(link)
    print(link)
    findItem()              # scrap first page

                        # move to next page
    while True:
        try:
            browser.execute_script("return arguments[0].scrollIntoView(true);",WebDriverWait(browser, 10).until(
             EC.presence_of_element_located((By.XPATH, "//a[@title = 'Next']"))
                 ))
            browser.find_element_by_xpath("//a[@title = 'Next']").click()

            print("------------------Next Page-----------------")
            # scraping the following pages
            findItem()
        except (TimeoutException, WebDriverException) as e:
            print("Last page reached")
            break

