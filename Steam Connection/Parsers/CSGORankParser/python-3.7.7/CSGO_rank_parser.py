import sys
import cloudscraper
from bs4 import BeautifulSoup as soup

totalArguments = len(sys.argv)

if totalArguments == 1:
    print('skillgroup_none')
else:
    url = 'https://csgostats.gg/player/' + sys.argv[1]
    scraper = cloudscraper.create_scraper(browser={
            'browser': 'firefox',
            'platform': 'windows',
            'mobile': False
        })
    html = scraper.get(url).text
    page_soup = soup(html, "html.parser")
    mainContainer = page_soup.find("div", {"class": "main-container"})
    images = mainContainer.find_all('img')
    rankImage = None
    for image in images:
        if str(image['src']).startswith('https://static.csgostats.gg/images/ranks'):
            rankImage = str(image['src'])
            break
    if rankImage is None:
        print('skillgroup_none')
    else:
        rank = rankImage.split('/')[-1].split('.')[0]
        print('skillgroup'+rank)