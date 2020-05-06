## Requirements

### Host setup

* [Docker Engine](https://docs.docker.com/install/)
* [Postman](https://www.postman.com/downloads/)

By default, the stack exposes the following ports so be sure it's not used:
* 5000: Logstash TCP input
* 9200: Elasticsearch HTTP
* 9300: Elasticsearch TCP transport
* 5601: Kibana
* 51529: LocationSearch API HTTP

## steps to run the solution
* clone the repository
* open terminal on the host machine as Administrator 
* CD to /LocationsSearch directory from the terminal
* run this command "docker-compose up" in the terminal
* the stack installation and data indexing will take around 5 minutes
* open postman and import Elastic.postman_collection.json collection to import the API end point
* you should find at postman new collection called "Elastic" and Post request called "Location search"
* you can use the Location search request to use the solution 



  