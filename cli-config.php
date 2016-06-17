<?php

use AllyTalks\ORM\DoctrineFactory;
use Doctrine\ORM\Tools\Console\ConsoleRunner;

require __DIR__.'/vendor/autoload.php';

$em = DoctrineFactory::buildEntityManagerFromConfig();

return ConsoleRunner::createHelperSet($em);