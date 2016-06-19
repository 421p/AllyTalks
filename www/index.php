<?php

declare(strict_types=1);

use AllyTalks\WebApp\Application;
use Symfony\Component\Debug\ErrorHandler;

require __DIR__ . '/../vendor/autoload.php';

ErrorHandler::register();

$app = new Application();
$app->run();
