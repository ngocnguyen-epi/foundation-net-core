import "bootstrap";
import "../css/main.scss";
require("easy-autocomplete");
import feather from "feather-icons";
import "lazysizes";
import "lazysizes/plugins/bgset/ls.bgset";
import FoundationCms from "./common/foundation.cms";

feather.replace();
window.feather = feather; 

var foundationCms = new FoundationCms();
foundationCms.init();