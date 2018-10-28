# Citfix
Citfix helps you get structured bibliographic reference in a easy way

Citfix is a cloud based structuring bibliographic references for the Publishers, Freelance editors, Services providers and the Authors. With its unique AI cloud based technology, Citfix can easily structure the bibliographic references, save time and cost for your editorial team. It assures 100% accuracy by verifying references with 240+ Million records as well as with PubMed and Crossref.

<b>Structuring:</b> Citfix can easily structure the references to XML, RIS and JAT XML formats.

<b>Link:</b> Automate linking to PubMed IDs and Crossref DOI's.

<b>Track:</b> Track changes on your references

<b>Style:</b> Submit any standard styles- AMA, NLM etc.

<b>Save time:</b> Automate tedious task such as linking and structuring

<b>Accuracy:</b> Citfix verifies 240+ million records, along with PubMed and Crossref

<h1>Citfix REST API</h1>
<h2>Preamble</h2>
The Citfix REST API is one of a variety of tools and APIs that allow the user to structure the reference in a sophisticated ways.

<h2>Supported format tags</h2>
<table>
  <tr>
    <th>Tag Name</th>
    <th>Description</th> 
  </tr>
  <tr>
    <td>&lt;b&gt;</td>
    <td>Bold</td> 
  </tr>
  <tr>
    <td>&lt;i&gt;</td>
    <td>Italic</td> 
  </tr>
  <tr>
    <td>&lt;u&gt;</td>
    <td>Underline</td> 
  </tr>
  <tr>
    <td>&lt;sc&gt;</td>
    <td>SMALL</td> 
  </tr>
  <tr>
    <td>&lt;skp&gt;</td>
    <td>SKIP</td> 
  </tr>
    <tr>
    <td>&lt;sup&gt;</td>
    <td>Superscript</td> 
  </tr>
    <tr>
    <td>&lt;sub&gt;</td>
    <td>Subscript</td> 
  </tr>
  </table>

<h2>Short tag description</h2>
<table>
  <tr>
    <th>Tag Name</th>
    <th>Description</th> 
  </tr>
  <tr>
    <td>ukn</td>
    <td>Unknown</td> 
  </tr>
    <tr>
    <td>del</td>
    <td>delimiter</td> 
  </tr>
    <tr>
    <td>deland</td>
    <td>Author/Editor delimiter 'and &'</td> 
  </tr>
  <tr>
    <td>snm</td>
    <td>Author/Editor Surname</td> 
  </tr>
  <tr>
    <td>gnm</td>
    <td>Author/Editor Given name</td> 
  </tr>
    <tr>
    <td>eds</td>
    <td>Author/Editor EDS</td> 
  </tr>
    <tr>
    <td>etal</td>
    <td>Author/Editor delimiter etal</td> 
  </tr>
    <tr>
    <td>suffix</td>
    <td>Author/Editor suffix</td> 
  </tr>
  <tr>
    <td>aug</td>
    <td>Author(s) group</td> 
  </tr>
    <tr>
    <td>edg</td>
    <td>Editor(s) group</td> 
  </tr>
    <tr>
    <td>ctl</td>
    <td>Chapter title</td> 
  </tr>
      <tr>
    <td>artno</td>
    <td>Article Number</td> 
  </tr>
    <tr>
    <td>atl</td>
    <td>Article title</td> 
  </tr>
    <tr>
    <td>btl</td>
    <td>Book title</td> 
  </tr>
    <tr>
    <td>jtl</td>
    <td>Journal title</td> 
  </tr>
    <tr>
    <td>vol</td>
    <td>Volume</td> 
  </tr>
    <tr>
    <td>iss</td>
    <td>Issue</td> 
  </tr>
   <tr>
    <td>pgg</td>
    <td>Page Group</td> 
  </tr>
    <tr>
    <td>spg</td>
    <td>Start page</td> 
  </tr>
    <tr>
    <td>epg</td>
    <td>End page</td> 
  </tr>
    <tr>
    <td>pub</td>
    <td>Publisher</td> 
  </tr>
      <tr>
    <td>loc</td>
    <td>Location Group</td> 
  </tr>
    <tr>
    <td>cty</td>
    <td>City</td> 
  </tr>
    <tr>
    <td>st</td>
    <td>State</td> 
  </tr>
  <tr>
    <td>cnt</td>
    <td>Country</td> 
  </tr>
    <tr>
    <td>col</td>
    <td>Collab</td> 
  </tr>
  <tr>
    <td>list</td>
    <td>OL/UL Label</td> 
  </tr>
  <tr>
    <td>mth</td>
    <td>Publish/Access/Create/Retrieved Month</td> 
  </tr>
   <tr>
    <td>day</td>
    <td>Publish/Access/Create/Retrieved Day</td> 
  </tr>
  <tr>
    <td>yr</td>
    <td>Publish/Access/Create/Retrieved Year</td> 
  </tr>
  <tr>
    <td>url</td>
    <td>URL</td> 
  </tr>
  <tr>
    <td>ssn</td>
    <td>Season</td> 
  </tr>
  <tr>
    <td>misc</td>
    <td>Misc</td> 
  </tr>
  <tr>
    <td>edn</td>
    <td>Edition</td> 
  </tr>
  <tr>
    <td>isbn</td>
    <td>ISBN</td> 
  </tr>
  <tr>
    <td>doi</td>
    <td>DOI</td> 
  </tr>
  <tr>
    <td>supp</td>
    <td>Suppliment</td> 
  </tr>
</table>

<h2>Limitation</h2>
<table>
  <tr>
    <th>Description</th>
    <th>Remarks</th> 
  </tr>
  <tr>
    <td>Client request limit</td>
    <td>Maximum number of references per request is limited to 250</td> 
  </tr>
  </table>

<h2>Integrate Citfix into your production workflow with API</h2>
If you’re looking to integrate bibliographic reference processing into a larger automated workflow, you can access all of Citfix’s capabilities programmatically using the Citfix REST API.
<h2>Reporting performance/availability, bugs, requesting features</h2>
Please report bugs with the API or the new feature please contact support@pubquick.com

<h2>License</h2>
<p>Citfix API is available under the <a href="http://opensource.org/licenses/MIT" rel="nofollow">MIT license</a>.</p>

