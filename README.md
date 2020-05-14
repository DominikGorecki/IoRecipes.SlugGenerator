# Generating URL and Human Friendly Strings

Useful for generating **unique** slugs to use in a url. 

Go from, `example.com/article/12` to `example.com/article/fast-fox`

## Examples
`Slug.GenerateSlug()`

**The Quick Fox!** - "the-quick-fox"

**@Fox&Friends** - "foxfriends"

## Filter out Existing

`Slug.SetExistingSlugs(IEnumerable<string>)`

Use this method to filter out existing slugs so that you don't have duplicates. So you can have:

`example.com/article/fast-fox`

`example.com/article/fast-fox-1`

`example.com/article/fast-fox-2`

etc. 

# FULL DOCUMENTATION
Available on iorecipes.com
