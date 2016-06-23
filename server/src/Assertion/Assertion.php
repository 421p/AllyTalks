<?php

namespace AllyTalks\Assertion;

use AllyTalks\Utils\Exception\JsonException;
use Assert\Assertion as BaseAssert;

class Assertion extends BaseAssert
{
    protected static $exceptionClass = JsonException::class;
}
