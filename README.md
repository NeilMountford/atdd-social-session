# ATDD and social testing session

This is the code for the basic session. The git commits correspond to the steps taken as part of the presentation (write failing test, make test pass, etc).

You can list the commit hashes using `git log --oneline` and look at a specific one using `git checkout <commit hash>`. I'd suggest you start with the oldest and work through to the newest.

## Testing approaches

There's a few ways to write the tests so I've included some examples of each (though not for every scenario):

- YaBasic - Aptly named as these tests read quite horribly in the test runner and the code
- Bddfy - Mostly using the fluent approach (rather than the convention based reflective approach, though there are a couple of examples of this). BDDfy produces nice output and can generate reports of this (both text and html)
- BDD style with no library - In the UseCases folder there's a folder for each when with a class for each given. Reads really nicely in the test runner but you lose out on the BDD style output

## Social testing

In order to socially test the components we're using `WebApplicationFactory` from the `Microsoft.AspNetCore.Mvc.Testing`. The tests for a data integration show an example of extending `WebApplicationFactory` to override some of the DI with a mock.