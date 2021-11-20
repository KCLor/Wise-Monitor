from selenium import webdriver
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
ref.child('Sunshine').delete()

# method for finding product name and price
def getItem():
    item = browser.find_elements_by_css_selector('div.p-body')

    for post in item:
        name = post.find_element_by_tag_name('h4').text
        price = post.find_element_by_class_name('p-price').text
        ref = db.reference('Sunshine')
        emp_ref = ref.push({
            'name': name,
            'price': price
        })

# delete the # in the list below to retrieve the products' name and price from the link
url = {
#'https://www.sunshineonline.com.my/fresh-produce',
#'https://www.sunshineonline.com.my/dairy-chilled',
#'https://www.sunshineonline.com.my/bakery',
#'https://www.sunshineonline.com.my/frozen',
#'https://www.sunshineonline.com.my/pantry',
#'https://www.sunshineonline.com.my/beverage',
#'https://www.sunshineonline.com.my/alcohol',
#'https://www.sunshineonline.com.my/health-beauty',
#'https://www.sunshineonline.com.my/skincare-cosmetic',
#'https://www.sunshineonline.com.my/baby-child',
#'https://www.sunshineonline.com.my/stationery',
#'https://www.sunshineonline.com.my/home-outdoor',
#'https://www.sunshineonline.com.my/household-pet',
#'https://www.sunshineonline.com.my/organic',
#'https://www.sunshineonline.com.my/pet'
'https://www.sunshineonline.com.my/coffee'
}
for link in url:
    browser.get(link)
    print(link)
    getItem()

    # scroll to bottom of the page to get unloaded item appear
    lastHeight = browser.execute_script("return document.body.scrollHeight")
    while True:
        browser.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        newHeight = browser.execute_script("return document.body.scrollHeight")
        if newHeight == lastHeight:
            break
        lastHeight = newHeight
    getItem()            # scrap item

    # Did included item that is out of stock
