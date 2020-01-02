# Logger Case Study

Implementation of Logging mechanism which is caching logs in "InMemoryCache". A periodic background task is running and writing these logs to database or file.
Main idea is trying to Log to database. Logging to file is running only if logging to database fail.
