<?php

namespace AllyTalks\ORM;

use Doctrine\Common\Persistence\ObjectManager;
use Doctrine\ORM\EntityManager;
use Doctrine\ORM\Tools\Setup;
use Nelmio\Alice\Fixtures;
use Symfony\Component\Yaml\Yaml;

class DoctrineFactory
{
    public static function buildEntityManagerFromConfig() : EntityManager
    {
        $isDevMode = true;

        $configuration = Setup::createAnnotationMetadataConfiguration(
            [__DIR__.'/Entities'],
            $isDevMode
        );

        $config = Yaml::parse(
            file_get_contents(
                $_ENV['IS_CI'] ? __DIR__.'/../../config/travis-config.yml' :
                    __DIR__.'/../../config/config.yml'
            )
        );
        $conn = $config['doctrine'];

        return EntityManager::create($conn, $configuration);
    }

    public static function loadFixtures(ObjectManager $om)
    {
        Fixtures::load(__DIR__.'/Fixtures/user.yml', $om);
    }
}
